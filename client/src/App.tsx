import React from 'react';
import './App.css';
import {BrowserRouter, Route, Routes} from 'react-router-dom';
import routes from './route-config'
import Header from './layout/Header';
import Footer from './layout/Footer';
import configureValidations from './Validations';
import axios from 'axios';
import { getCurrentUser } from './services/auth.service';

configureValidations();

function App() {
  //Подкладываем токен в каждый запрос
  axios.interceptors.request.use(config => {
    const token = localStorage.getItem("token");  
    if(token)
      config.headers["Authorization"] = `Bearer ${token}`;
    return config;
  });
  return ( 
    <>
      <BrowserRouter>
      <Header />
      <div className='container'>
      <Routes>
          {routes.map(({ path, element }) =>
            <Route key={path} path={path} element={element} />
          )}
        </Routes>
      </div>
      </BrowserRouter>
      <Footer />
    </>
  );
}

export default App;

