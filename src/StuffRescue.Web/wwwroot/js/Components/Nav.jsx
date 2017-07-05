import Brand from './Brand.jsx';
import AddStuffCam from './AddStuffCam.jsx';
import HeadShot from './HeadShot.jsx';
import Notification from './Notification.jsx';
import LoginPartial from './LoginPartial.jsx'

const Nav = (props) => {
    const {IsSignedIn, User, Title, SearchBarSize, Text, Store, AntiforgeryToken} = props;

    if (SearchBarSize === 'FULL') {
        return (
            <SearchFullScreenForm Store={Store} Text={Text} />
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
                                <Search Store={Store} Text={Text} />
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
                        <LoginPartial IsSignedIn={IsSignedIn} User={User} Title={Title} AntiforgeryToken={AntiforgeryToken} />
                        <ul className="nav navbar-nav">
                            <li className="active"><a href="/Home/Index">Home</a></li>
                        </ul>
                    </div>
                </div>
            </div>
        );
    }
};

export default Nav;