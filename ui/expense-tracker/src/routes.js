import Home from './Practice/Home'
import About from './Practice/About'
import Test from './Practice/Test'
import Login from './components/Login'
import ListExpenses from './components/Employee/ListExpenses'

const routes = [
    {
        path: "/home/:userId/tickets/:ticketId",
        element: <Home />
    },
    {
        path: "/about/",
        element: <ListExpenses />
    },
    {
        path: "/test",
        element: <Test />
    },
    {
        path: "/login",
        element: <Login />
    }
]

export default routes;