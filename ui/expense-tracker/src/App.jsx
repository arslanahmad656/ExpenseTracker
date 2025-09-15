import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap/dist/js/bootstrap.bundle.js'
import { Routes, Route, useLocation } from 'react-router-dom';
import routes from './routes';
import TitleBar from './components/TitleBar/TitleBar';
import { useSelector } from 'react-redux';

function App() {

  const { username, role, isLoggedIn } = useSelector(st => st.auth);
  const location = useLocation();
  
  const shouldRenderTitleBar = isLoggedIn && location.pathname !== '/login';

  const titleBarItems = shouldRenderTitleBar ? [
    ... getListItemsForRole(role),
    { text: 'Sign Out', iconClass: 'bi bi bi-box-arrow-left me-2 text-danger', action: () => console.log('ITEM 3')}
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

function getListItemsForRole(role) {
  switch (role) {
    case 'Employee': return [
      { text: 'Your Expenses', iconClass: 'bi bi-list-ul me-2 mt-1' },
      { text: 'New Expense', iconClass: 'bi bi-plus me-2 mt-1' }
    ];

    case 'Manager': return [
      { text: 'Pending Approvals', iconClass: 'bi bi-list-ul me-2 mt-1' },
      { text: 'Past Requests', iconClass: 'bi bi-list-ul me-2 mt-1' }
    ];

    case 'Accountant': return [
      { text: 'Pending Reimbursements', iconClass: 'bi bi-list-ul me-2 mt-1' },
      { text: 'Reimbursed Expenses', iconClass: 'bi bi-list-ul me-2 mt-1' }
    ];

    default: return [];
  }
}

export default App;
