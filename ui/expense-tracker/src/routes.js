import Home from './Practice/Home'
import About from './Practice/About'
import Test from './Practice/Test'
import Login from './components/Login'
import ExpenseForm from './components/Expense/ExpenseForm'
import ExpenseDetailsView from './components/Expense/Details/ExpenseDetailsView'
import UpdateExpenseForm from './components/Expense/UpdateExpenseForm'
import ManagerExpenseForm from './components/Expense/ManagerExpenseForm'
import AccountantExpenseForm from './components/Expense/AccountantExpenseForm'

const routes = [
    {
        path: "/home/:userId/tickets/:ticketId",
        element: <Home />
    },
    {
        path: "/about",
        element: <UpdateExpenseForm  formId={3} onSubmit={function() {
            console.log('check this SUBMIT', arguments);
            alert('check the arguments for submit in console.')           
        }} onCancelForm={function() {
            console.log('check this for cancellation FORM', arguments);
            alert('check the arguments for cancellation in console.')           
        }} onCancelExpense={function() {
            console.log('check this for cancellation expense', arguments);
            alert('check the arguments for cancellation expense in console.')           
        }}
         />
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
        element: null,
    },
    {
        path: "/form/list/accountant",
        element: null,
    },
    {
        path: "/form/list/manager",
        element: null,
    }
]

export default routes;