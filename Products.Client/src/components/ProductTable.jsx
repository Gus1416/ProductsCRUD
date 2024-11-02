import PropTypes from 'prop-types';
import { Button, Table } from 'react-bootstrap';
import { Pencil, Trash } from 'lucide-react';

const ProductTable = ({ products, onEdit, onDelete }) => {

    return (
        <>
            <Table striped bordered hover>
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>Nombre</th>
                        <th>Unidades</th>
                        <th>Precio</th>
                        <th>Acciones</th>
                    </tr>
                </thead>
                <tbody>
                    {products.map(product => (
                        <tr key={product.id}>
                            <td>{product.id}</td>
                            <td>{product.name}</td>
                            <td>{product.quantity}</td>
                            <td>{product.price.toFixed(2)}</td>
                            <td>
                                <Button variant="primary" size="sm" className="me-2" onClick={() => onEdit(product)}>
                                    <Pencil size={16} className="mb-1" />
                                    <span className="visually-hidden">Edit {product.name}</span>
                                </Button>
                                <Button variant="danger" size="sm" onClick={() => onDelete(product.id)}>
                                    <Trash size={16} className="mb-1" />
                                    <span className="visually-hidden">Delete {product.name}</span>
                                </Button>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </Table>
        </>
    )
}

ProductTable.propTypes = {
    products: PropTypes.arrayOf(
        PropTypes.shape({
            id: PropTypes.number.isRequired,
            name: PropTypes.string.isRequired,
            quantity: PropTypes.number.isRequired,
            price: PropTypes.number.isRequired
        })
    ).isRequired,
    onEdit: PropTypes.func.isRequired,
    onDelete: PropTypes.func.isRequired,
};

export default ProductTable