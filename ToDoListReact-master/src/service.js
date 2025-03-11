import axios from 'axios';
import api from './axiosInterceptor';


const apiUrl = process.env.REACT_APP_MY_API;

export default {
  
  //שליפת כל המשימות
  getTasks: async () => {
    //הפונקציה get מופעלת על api שמזומן למעלה 
    const result = await api.get(`${apiUrl}/items`)
     return result.data;
  },

//הוספת משימה
  addTask: async(name)=>{
    const result = await api.post(`${apiUrl}/additem`,{ name }, {
      headers: { "Content-Type": "application/json" }
  })    
    return result.data;
  },
//עדכון משימה
  setCompleted: async(id, isComplete)=>{
    console.log('setCompleted', {id, isComplete})
    const result = await api.put(`${apiUrl}/updateItem/${id}`,{isComplete})

    return result.data;
  },
//מחיקת משימה
  deleteTask:async(id)=>{
    console.log('deleteTask')
    const result = await api.delete(`${apiUrl}/del/${id}`)    
    return result.data;
    
  }
};

