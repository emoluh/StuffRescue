const listClass = (title) => {
    if (title === "Manage your account"
        || title === "Register"
        || title === "Change Password"
        || title === "Add Stuff"
        || title === "Log in"
        || title === "Forgot your password?")
        return "active";
    else
        return "";
};

const LoginPartial = (IsSignIn, User, Title) => {
    if (IsSignIn) {
        return (
            <form href="/Account/LogOff" method="post" id="logoutForm">
                <ul className="nav navbar-nav">
                    <li className={listClass(Title)}>
                        <a className="dropdown-toggle" dataToggle="dropdown">Hello {User}!</a>
                        <ul className="dropdown-menu">
                            <li className={listClass(Title)}><a href="/Manage/Index">My Account</a></li>
                            <li className={listClass(Title)}><a href="/Manage/ChangePassword">Change Password</a></li>
                            <li><a href="javascript:document.forms.item(0).submit();">Sign Out</a></li>
                        </ul>
                    </li>
                </ul>
                <ul className="nav navbar-nav">
                    <li className={listClass(Title)}><a href="/Stuff/Index">Add Stuff</a></li>
                </ul>
            </form>
        );
    } else {
        return (
            <ul class="nav navbar-nav">
                <li className={listClass(Title)}><a href="/Account/Register">Register</a></li>
                <li className={listCLass(Title)}><a href="/Account/Login">Log in</a></li>
            </ul>
        );
    }
};

export default LoginPanel;