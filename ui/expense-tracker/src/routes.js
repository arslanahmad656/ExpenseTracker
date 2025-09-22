import Home from './Practice/Home'
import About from './Practice/About'
import Test from './Practice/Test'
import Login from './components/Login'
import ExpenseForm from './components/Expense/ExpenseForm'
import ExpenseDetailsView from './components/Expense/Details/ExpenseDetailsView'
import UpdateExpenseForm from './components/Expense/UpdateExpenseForm'
import ManagerExpenseForm from './components/Expense/ManagerExpenseForm'
import AccountantExpenseForm from './components/Expense/AccountantExpenseForm'
import ExpensesList from './components/Expense/ExpensesList'

const routes = [
    {
        path: "/home/:userId/tickets/:ticketId",
        element: <Home />
    },
    {
        path: "/about",
        element: <ExpensesList />
    },
    {
        path: "/test",
        element: <Test />
    },
    {
        path: "/login",
        element: <Login />
    },
    {
        path: "/form/create",
        element: <ExpenseForm />
    },
    {
        path: "/form/:formId/details",
        element: <ExpenseDetailsView />
    },
    {
        path: "/form/details",
        element: <ExpenseDetailsView />
    },
    {
        path: "/form/:formId/edit",
        element: <UpdateExpenseForm />
    },
    {
        path: "/form/:formId/managerial",
        element: <ManagerExpenseForm />
    },
    {
        path: "/form/:formId/accountant",
        element: <AccountantExpenseForm />
    },
    {
        path: "/form/list/employee",
        element: <ExpensesList />,
    },
    {
        path: "/form/list/accountant",
        element: <ExpensesList />,
    },
    {
        path: "/form/list/manager",
        element: <ExpensesList />,
    }
]

export default routes;