import axios from 'axios';

const axiosInstance = axios.create({
    baseURL: "https://localhost:7241/api/Products",
    withCredentials: true
});

export default axiosInstance;