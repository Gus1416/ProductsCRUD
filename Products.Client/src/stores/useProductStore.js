import { create } from 'zustand';
import axios from '../config/axios.js';

export const useProductStore = create((set) => ({
    products: [],

    fetchAllProducts: async () => {
        try {
            const response = await axios.get("/get_all_products");
            set({ products: response.data });
            console.log("Datos obtenidos", response.data);

        } catch (error) {
            console.error(error.message);
            alert("Error fecthing products: " + error.message);
        }
    },

    addProduct: async (newProduct) => {
        try {
            const response = await axios.post("/create", newProduct);
            set((prevState) => ({
                products: [...prevState.products, response.data]
            }));
            alert("Producto agregado exitosamente");
        } catch (error) {
            console.error("Error al agregar el producto: ", error.message);
            alert("Error al agregar el producto: " + error.message);
        }
    },

    updateProduct: async (updatedProduct) => {
        try {
            const { id } = updatedProduct;
            const response = await axios.put(`/update?id=${id}`, updatedProduct);
            set((prevState) => ({
                products: prevState.products.map((product) =>
                    product.id === id ? response.data : product
                )
            }));
            alert("Producto actualizado correctamente");
        } catch (error) {
            console.error("Error actualizando producto: ", error.message);
            alert("Error actualizando producto: " + error.message);
        }
    },

    deleteProduct: async (productId) => {
        try {
            await axios.delete(`/delete?id=${productId}`);
            set(prevState => ({
                products: prevState.products.filter(product => product.id !== productId)
            }));
            alert("Producto eliminado exitosamente");
        } catch (error) {
            console.error("Error eliminando el producto: ", error.message);
            alert("Error eliminando el producto: " + error.message);
        }
    }
}))