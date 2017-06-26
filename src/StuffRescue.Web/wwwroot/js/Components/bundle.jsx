const AddStuffCam = () => (
    <div style={{ marginTop: "20px" }}>
        <a href="/Home/Index">
            <img src="/images/add.stuff.cam.png" style={{ height: "40px" }} alt="Camera" />
        </a>
    </div>
);

const Brand = () => (
    <div style={{ marginTop: "20px", marginRight: "20px" }}>
        <a href="/Home/Index">
            <img src="/images/sr.logo.mainbar.png" style={{ height: "40px" }} alt="Company Logo" />
        </a>
    </div>
);

const HeadShot = () => (
    <div style={{ marginTop: "20px", marginRight: "20px" }}>
        <a href="/Home/Index">
            <img src="/images/profile.notloggedin.png" style={{ height: "40px" }} alt="Headshot" />
        </a>
    </div>
);

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

const LoginPartial = (props) => {
    const {IsSignedIn, User, Title} = props;

    if (IsSignedIn) {
        return (
            <form href="/Account/LogOff" method="post" id="logoutForm">
                <ul className="nav navbar-nav">
                    <li className={listClass(Title)}>
                        <a className="dropdown-toggle" data-toggle="dropdown">
                            <span>Hello! {User}</span>
                        </a>
                        <ul className="dropdown-menu">
                            <li key={"0"} className={listClass(Title)}><a href="/Manage/Index">My Account</a></li>
                            <li key={"1"} className={listClass(Title)}><a href="/Manage/ChangePassword">Change Password</a></li>
                            <li key={"2"}><a href="javascript:document.forms.item(0).submit();">Sign Out</a></li>
                        </ul>
                    </li>
                </ul>
                <ul className="nav navbar-nav">
                    <li key={"0"} className={listClass(Title)}><a href="/Stuff/Index">Add Stuff</a></li>
                </ul>
            </form>
        );
    } else {
        return (
            <ul className="nav navbar-nav">
                <li key={"0"} className={listClass(Title)}><a href="/Account/Register">Register</a></li>
                <li key={"1"} className={listClass(Title)}><a href="/Account/Login">Log in</a></li>
            </ul>
        );
    }
};

const Notification = () => (
    <div style={{ marginTop: "20px", marginRight: "20px" }}>
        <span className="badge">4</span>
    </div>
);

class SearchField extends React.Component {
    render() {
        return (
            <div className="search">
                <input type="text"
                    placeholder="What are you looking to rescue?"
                    name={this.props.name}
                    autoComplete="off"
                    onChange={this.props.onChange} />
            </div>
        );
    }
}

class SearchFullScreen extends React.Component {
    render() {
        return (
            <div className="full-screen-search">
                <button className="close-button"
                    onClick={this.props.onClose}>X</button>
                <input type="text"
                    name="searchlarge"
                    onChange={this.props.onChange} />
            </div>
        );
    }
}

class SearchFullScreenForm extends React.Component {
    constructor(props) {
        super(props);
        var Store = props.Store;

        this.state = {
            showLargeSearch: {
                display: 'block'
            }
        };

        this._handleChange = this._handleChange.bind(this);
        this._handleClose = this._handleClose.bind(this);
    }

    componentDidMount() {
        this._form.searchlarge.focus()
    }

    _handleChange(e) {
        e.preventDefault();
        let n = e.target.name;
        if (e.target.value.length > 0) {
            this.setState({ showLargeSearch: { display: 'block' } }, () => {
                this._form.searchlarge.focus();
            });
            this._form.searchlarge.value = e.target.value;
        }
        else {
            this._handleClose(e);
        }
    }

    _handleClose(e) {
        e.preventDefault();
        this.setState({ showLargeSearch: { display: 'none' } }, () => {
            this._form.search.focus();
        });
        Store.dispatch({ type: 'SMALL' });
    }

    render() {
        return (
            <div>
                <form ref={f => this._form = f}>
                    <div style={this.state.showLargeSearch}>
                        <SearchFullScreen
                            onChange={this._handleChange}
                            onClose={this._handleClose} />
                    </div>
                </form>
            </div>
        );

    }
}

class Search extends React.Component {
    constructor(props) {
        super(props);
        var Store = props.Store;

        this.state = {
            showLargeSearch: {
                display: 'none'
            }
        };

        this._handleChange = this._handleChange.bind(this);
        this._handleClose = this._handleClose.bind(this);
    }
    _handleChange(e) {
        e.preventDefault();
        Store.dispatch({ type: 'FULL' });
    }

    _handleClose(e) {
        e.preventDefault();
        Store.dispatch({ type: 'SMALL' });
    }

    render() {
        return (
            <div>
                <form ref={f => this._form = f}>
                    <SearchField
                        name="search"
                        onChange={this._handleChange} />
                    <div style={this.state.showLargeSearch}>
                        <SearchFullScreen
                            onChange={this._handleChange}
                            onClose={this._handleClose} />
                    </div>
                </form>
            </div>
        );
    }
}

const Nav = (props) => {
    const {IsSignedIn, User, Title, SearchBarSize} = props;

    if (SearchBarSize === 'FULL') {
        return (
            <SearchFullScreenForm Store={Store}/>
        );
    } else {
        return (
            <div className="navbar navbar-inverse navbar-fixed-top topbar">
                <div className="container">
                    <div className="navbar-header">
                        <div className="form-group">
                            <div className=" col-md-1">
                                <button type="button" className="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                                    <span className="sr-only">Toggle navigation</span>
                                    <span className="icon-bar"></span>
                                    <span className="icon-bar"></span>
                                    <span className="icon-bar"></span>
                                </button>
                            </div>
                            <div className="col-md-2" style={{ marginRight: '20px' }}>
                                <Brand />
                            </div>
                            <div className="col-md-4">
                                <Search Store={Store} />
                            </div>
                            <div className="col-md-1">
                                <AddStuffCam />
                            </div>
                            <div className="col-md-2">
                                <HeadShot />
                            </div>
                            <div className="col-md-1">
                                <Notification />
                            </div>
                        </div>
                    </div>
                    <div className="navbar-collapse collapse">
                        <LoginPartial IsSignedIn={IsSignedIn} User={User} Title={Title} />
                        <ul className="nav navbar-nav">
                            <li className="active"><a href="/Home/Index">Home</a></li>
                        </ul>
                    </div>
                </div>
            </div>
        );
    }
};
