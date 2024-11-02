import { useEffect, useState } from 'react';
import { Button } from 'react-bootstrap';
import { Plus } from 'lucide-react';
import { useProductStore } from '../stores/useProductStore';
import ProductTable from '../components/ProductTable';
import ProductForm from '../components/ProductForm';

const InventoryPage = () => {
    const { fetchAllProducts, products, deleteProduct } = useProductStore();

    const [showModal, setShowModal] = useState(false);
    const [currentProduct, setCurrentProduct] = useState(null);

    useEffect(() => {
        fetchAllProducts();
    }, [fetchAllProducts]);

    const handleAddProduct = () => {
        setCurrentProduct(null);
        setShowModal(true);
    }

    const handleEditProduct = (product) => {
        setCurrentProduct(product);
        setShowModal(true);
    }

    const handleDeleteProduct = (productId) => {
        deleteProduct(productId)
    }

    const handleCloseModal = () => {
        setShowModal(false);
    }

    return (
        <>
            <div className="d-flex justify-content-between align-items-center mb-4">
                <h1>Inventario de Productos</h1>
                <Button variant="success" onClick={handleAddProduct} >
                    <Plus size={16} className="me-2" />
                    Agregar producto
                </Button>
            </div>
            <ProductTable
                products={products}
                onEdit={handleEditProduct}
                onDelete={handleDeleteProduct}
            />
            <ProductForm
                show={showModal}
                onClose={handleCloseModal}
                isNewProduct={!currentProduct}
                currentProduct={currentProduct}
            />
        </>
    )
}

export default InventoryPage