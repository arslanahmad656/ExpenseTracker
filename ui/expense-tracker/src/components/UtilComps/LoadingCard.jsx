export default function LoadingCard({message = 'Loading...'}) {
    return (
        <div className="card border-0 shadow-sm">
            <div className="card-body">
                {message}
            </div>
        </div>
    )
}