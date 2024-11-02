import { Routes, Route } from 'react-router-dom';
import InventoryPage from './pages/InventoryPage';

function App() {
    return (
      <div className="container mt-5">
            <Routes>
                <Route path="/" element={<InventoryPage />} />
            </Routes>
      </div>
  )
}

export default App
