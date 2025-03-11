import express from 'express'

const app=express()
const port=3001
import renderApi from '@api/render-api';

app.get('',(req,res)=>{
    renderApi.auth('rnd_CV9t3ChNT7d5c70OiOHUB9RkQ61L');
    renderApi.listServices({includePreviews: 'true', limit: '20'})
      .then(({ data }) =>res.send(data))
      .catch(err => console.error(err)); 
})


app.listen(port,()=>{
    console.log(`open !!!!!!!!!!!!!! ${port}`);
    
})