import React from 'react';
import './App.css';
import {BrowserRouter, Route, Routes} from 'react-router-dom';
import routes from './route-config'
import Header from './layout/Header';
import Footer from './layout/Footer';

function App() {
  return ( 
    <>
      <Header />
      <BrowserRouter>
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
