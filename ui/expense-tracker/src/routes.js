import Home from './Practice/Home'
import About from './Practice/About'
import Test from './Practice/Test'
import Login from './components/Login'
import ListExpenses from './components/Employee/ListExpenses'
import ExpenseForm from './components/Expense/ExpenseForm'
import ExpenseDetailsView from './components/Expense/Details/ExpenseDetailsView'
import { ExpenseStatus, FormStatus } from './utils/enums'

const routes = [
    {
        path: "/home/:userId/tickets/:ticketId",
        element: <Home />
    },
    {
        path: "/about",
        element: <ExpenseDetailsView 
            title={'Client Meeting'}
            currency={'USD'}
            status={FormStatus.Rejected}
            trackingId={'track0x99682'}
            lastUpdatedOn={'2025-04-25'}
            rejectionReason={'some reason to cancel the entire form'}
            expenses={[
                {
                    description: 'expense description',
                    amount: 500.25,
                    date: '2025-09-15',
                    status: ExpenseStatus.Reimbursed,
                    trackingId: 'xyz',
                    lastUpdatedOn: '2025-09-12'
                },
                {
                    description: 'expense description',
                    amount: 500.25,
                    date: '2025-09-15',
                    status: ExpenseStatus.Cancelled,
                    trackingId: 'xyz',
                    lastUpdatedOn: '2025-09-12'
                },
                {
                    description: 'expense description',
                    amount: 500.25,
                    date: '2025-09-15',
                    status: ExpenseStatus.PendingApproval,
                    trackingId: 'xyz',
                    lastUpdatedOn: '2025-09-12'
                },
                {
                    description: 'expense description',
                    amount: 500.25,
                    date: '2025-09-15',
                    status: ExpenseStatus.PendingReimbursement,
                    trackingId: 'xyz',
                    lastUpdatedOn: '2025-09-12'
                },
                {
                    description: 'expense description',
                    amount: 500.25,
                    date: '2025-09-15',
                    status: ExpenseStatus.Rejected,
                    rejectionReason: 'Personal expense is not reimbursable',
                    trackingId: 'xyz',
                    lastUpdatedOn: '2025-09-12'
                }
            ]}
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
    }
]

export default routes;