import Home from './Practice/Home'
import About from './Practice/About'
import Test from './Practice/Test'
import Login from './components/Login'
import ListExpenses from './components/Employee/ListExpenses'
import NewExpenseForm from './components/Expense/NewExpenseForm'

const routes = [
    {
        path: "/home/:userId/tickets/:ticketId",
        element: <Home />
    },
    {
        path: "/about",
        element: <NewExpenseForm />
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
        path: "/expense/create",
        element: <NewExpenseForm />
    }
]

export default routes;