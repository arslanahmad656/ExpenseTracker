import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap/dist/js/bootstrap.bundle.js'
import { Routes, Route, useLocation, useNavigate } from 'react-router-dom';
import routes from './routes';
import TitleBar from './components/TitleBar/TitleBar';
import { useDispatch } from 'react-redux';
import authService from './api/authService';
import { logout } from './store/authSlice';

function App() {
  // const { username, role, isLoggedIn } = useSelector(st => st.auth);
  const location = useLocation();
  const dispatch = useDispatch();
  const navigate = useNavigate();
  
  const isLoggedIn = authService.getAuthenticatedRole() != null;
  const shouldRenderTitleBar = isLoggedIn && location.pathname !== '/login';
  
  const username = authService.getAuthenticatedUsername();
  const role = authService.getAuthenticatedRole();

  const titleBarItems = shouldRenderTitleBar ? [ 
    ... getListItemsForRole(role, navigate),
    { text: 'Sign Out', iconClass: 'bi bi-box-arrow-left me-2 text-danger', action: () => {
      authService.clearSession();
      dispatch(logout());
      navigate('/login');
    }}
  ] : [];

  return (
    <div className="container pt-4">
      <>
        {shouldRenderTitleBar && <TitleBar user={{username: username, role: role }} titleBarItems={titleBarItems} />}
        <Routes>
          {
            routes.map((route, idx) => (
              <Route key={idx} path={route.path} element={route.element} />
            ))
          }
        </Routes>
      </>
    </div>
  )
}

function getListItemsForRole(role, navigate) {
  switch (role) {
    case 'Employee': return [
      { text: 'Your Expenses', iconClass: 'bi bi-list-ul me-2 mt-1', action: () => navigate('/form/list/employee') },
      { text: 'New Expense', iconClass: 'bi bi-plus me-2 mt-1', action: () => navigate('/form/create') }
    ];

    case 'Manager': return [
      { text: 'Approval Requests', iconClass: 'bi bi-list-ul me-2 mt-1', action: () => navigate('/form/list/manager') }
    ];

    case 'Accountant': return [
      { text: 'Pending Reimbursements', iconClass: 'bi bi-list-ul me-2 mt-1', action: () => navigate('/form/list/accountant') }
    ];

    default: return [];
  }
}

export default App;
