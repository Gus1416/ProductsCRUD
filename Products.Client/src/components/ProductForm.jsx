import PropTypes from 'prop-types';
import { useEffect, useState } from 'react';
import { Modal, Form, Button } from 'react-bootstrap';
import { useProductStore } from '../stores/useProductStore';

const ProductForm = ({ show, onClose, isNewProduct, currentProduct }) => {
    const { addProduct, updateProduct } = useProductStore();

    const [productData, setProductData] = useState({
        id: 0,
        name: '',
        quantity: 0,
        price: 0
    });

    useEffect(() => {
        if (currentProduct) {
            setProductData({
                id: currentProduct.id,
                name: currentProduct.name,
                quantity: currentProduct.quantity,
                price: currentProduct.price
            })

        } else {
            setProductData({
                id: 0,
                name: '',
                quantity: 0,
                price: 0
            })
        }
    }, [currentProduct]);

    const handleChange = (e) => {
        const { name, value } = e.target;
        setProductData((prevData) => ({ ...prevData, [name]: value }));
    };

    const handleSubmit = (e) => {
        e.preventDefault();
        if (isNewProduct) {
            addProduct(productData)
        } else {
            updateProduct(productData)
        }
        onClose();
    };

    return (
        <Modal show={show} onHide={onClose}>
            <Modal.Header closeButton>
                <Modal.Title>{isNewProduct ? 'Agregar Nuevo Producto' : 'Editar Product'}</Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <Form onSubmit={handleSubmit}>
                    <Form.Group className="mb-3">
                        <Form.Label htmlFor="name">Nombre</Form.Label>
                        <Form.Control
                            type="text"
                            id="name"
                            name="name"
                            value={productData.name}
                            onChange={handleChange}
                            required
                        />
                    </Form.Group>               
                    <Form.Group className="mb-3">
                        <Form.Label htmlFor="price">Precio</Form.Label>
                        <Form.Control
                            type="number"
                            id="price"
                            name="price"
                            value={productData.price}
                            onChange={handleChange}
                            step="0.01"
                            required
                        />
                    </Form.Group>
                    <Form.Group className="mb-3">
                        <Form.Label htmlFor="stock">Cantidad</Form.Label>
                        <Form.Control
                            type="number"
                            id="quantity"
                            name="quantity"
                            value={productData.quantity}
                            onChange={handleChange}
                            required
                        />
                    </Form.Group>
                    <Button variant="primary" type="submit">
                        {isNewProduct ? 'Agregar Producto' : 'Guardar Cambios'}
                    </Button>
                </Form>
            </Modal.Body>
        </Modal>
    )
}

ProductForm.propTypes = {
    show: PropTypes.bool.isRequired,
    onClose: PropTypes.func.isRequired,
    isNewProduct: PropTypes.bool.isRequired,
    currentProduct: PropTypes.shape({
        id: PropTypes.number,
        name: PropTypes.string,
        quantity: PropTypes.number,
        price: PropTypes.number,
    })
};

export default ProductForm