import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap/dist/js/bootstrap.bundle.js'
import { Routes, Route } from 'react-router-dom';
import routes from './routes';

function App() {
  return (
    <div className="container mt-4">
      <Routes>
        {
          routes.map((route, idx) => (
            <Route key={idx} path={route.path} element={route.element} />
          ))
        }
      </Routes>
    </div>
  )
}

export default App;
