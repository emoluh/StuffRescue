import Brand from './Brand.jsx';
import AddStuffCam from './AddStuffCam.jsx';
import HeadShot from './HeadShot.jsx';
import Notification from './Notification.jsx';
import LoginPartial from './LoginPartial.jsx'

const Nav = (IsSignedIn, User, Title) =>
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
                    <div className="col-md-2" styles={{ marginRight: '20px'}}>
                        <Brand />
                    </div>
                    <div className="col-md-4">
                        Search
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
                <LoginPartial />
                <ul className="nav navbar-nav">
                    <li className="active"><a href="/Home/Index">Home</a></li>
                </ul>
            </div>
        </div>
    </div>;

ReactDOM.render(<Nav IsSignedIn={true} User={"Insik"} Title={"Manage your account"} />, document.getElementById('nav'));
