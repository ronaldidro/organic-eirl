import axios from "axios";

const API_BASE_URL = import.meta.env.PROD
  ? "/api"
  : "http://localhost:5056/api";

export const api = axios.create({
  baseURL: API_BASE_URL,
  headers: {
    "Content-Type": "application/json",
  },
});

api.interceptors.response.use(
  (response) => response,
  (error) => {
    return Promise.reject(error);
  }
);
