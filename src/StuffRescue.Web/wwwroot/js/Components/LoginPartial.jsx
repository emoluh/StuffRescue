const LoginPartial = (props) => {
    const {IsSignedIn, User, Title, AntiforgeryToken} = props;

    if (IsSignedIn) {
        return (
            <form action="/Account/LogOff" method="post" id="logoutForm">
                <ul className="nav navbar-nav">
                    <li className={listClass(Title)}>
                        <a className="dropdown-toggle" data-toggle="dropdown">
                            <span>Hello! {User}</span>
                        </a>
                        <ul className="dropdown-menu">
                            <li key={"0"} className={listClass(Title)}>
                                <a href="/Manage/Index">My Account</a>
                            </li>
                            <li key={"1"} className={listClass(Title)}>
                                <a href="/Manage/ChangePassword">Change Password</a>
                            </li>
                            <li key={"2"}>
                                <a href="javascript:document.forms.item(1).submit();">Sign Out</a>
                            </li>
                        </ul>
                    </li>
                </ul>
                <ul className="nav navbar-nav">
                    <li key={"0"} className={listClass(Title)}>
                        <a href="/Stuff/Index">Add Stuff</a>
                    </li>
                </ul>
                <input type="hidden" name="__RequestVerificationToken" value={AntiforgeryToken} />
            </form>
        );
    } else {
        return (
            <ul className="nav navbar-nav">
                <li key={"0"} className={listClass(Title)}>
                    <a href="/Account/Register">Register</a>
                </li>
                <li key={"1"} className={listClass(Title)}>
                    <a href="/Account/Login">Log in</a>
                </li>
            </ul>
        );
    }
};

export default LoginPartial;
