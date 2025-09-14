import { useNavigate } from "react-router-dom"

export default function Test() {
    const navigate = useNavigate();

    return (
        <div className="container mt-4 p-5">
            <button class="btn btn-primary" onClick={() => navigate(`/home/${10}/tickets/${'tkt111'}?detailed=${true}`, { state: {meta: "xyz"}})}>Link 1</button>
            <button class="btn btn-primary" onClick={() => navigate("/about")}>Link 2</button>
            <button class="btn btn-primary" onClick={() => navigate("/test")}>Link 3</button>
        </div>
    )
}