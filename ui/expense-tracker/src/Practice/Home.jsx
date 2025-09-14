import { useLocation, useParams, useSearchParams } from "react-router-dom";

export default function Home() {
    
    const {userId, ticketId} = useParams();
    const [searchParams] = useSearchParams();
    const location = useLocation();

    return (
        <>
            <h1 class="h1">Home Page</h1>

            <ul className="list-group list-group-flush">
                <div className="list-group-item list-group-item-primary">UserID: {userId}</div>
                <div className="list-group-item">TicketID: {ticketId}</div>
                <div className="list-group-item">Detailed: {searchParams.get("detailed")}</div>
                <div className="list-group-item">Meta: {location.state?.meta}</div>
            </ul>
        </>
    )

}