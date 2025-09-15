import { useNavigate } from "react-router-dom"
import { registerCallback } from "../utils/callbackRegistry";

export default function Test() {
    const navigate = useNavigate();

    return (
        <div className="container mt-4 p-5">
            <button class="btn btn-primary" onClick={() => navigate(`/home/${10}/tickets/${'tkt111'}?detailed=${true}`, { state: {meta: "xyz"}})}>Link 1</button>
            <button class="btn btn-primary" onClick={() => navigate("/about")}>Link 2</button>
            <button class="btn btn-primary" onClick={() => {
                debugger;
                const callbackId = 'create-expense-form-submit';
                registerCallback(callbackId, function (payload) {
                    console.log('Form has been submitted with payload:', payload);
                });
                navigate("/expense/create", { state: {onSubmitCallbackId: callbackId}});
            }}>Link 3</button>
        </div>
    )
}