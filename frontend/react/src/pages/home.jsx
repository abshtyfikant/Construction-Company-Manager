import { Link } from "react-router-dom";

export default function HomePage () {

    return (
        <div>
            <h1>Witaj</h1>
            <Link to='/login'>Zaloguj się</Link>
        </div>
    );
}