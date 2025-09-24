export default function Alert({message, type, classes = ''}) {
    return (
        <div className={`alert alert-${type} ${classes}`} role="alert">
            <strong>{message}</strong>
        </div>
    )
}