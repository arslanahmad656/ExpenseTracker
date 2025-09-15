export default function TitleBarItem({ text, action, iconClass }) {
    return (<button 
        className="dropdown-item d-flex align-items-center"
        onClick={action}
    >
        <span>
            { iconClass && <i className={iconClass}></i>}
            <span>{text}</span>
        </span>
    </button>)
}