import axios from "axios";


const api = axios.create({
    baseURL: "https://localhost:5001", 
});

//interceptors - לתפיסת שגיאות
api.interceptors.response.use(
    (response) => {
        console.log("good");
        return response;
    },
    (error) => {
        console.error("Error:","kkkkkkk");
        console.error("Error:", error.data);
        return Promise.reject(error);
    }
);

export default api;