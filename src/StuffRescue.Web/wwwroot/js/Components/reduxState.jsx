const searchStatus = (state = { size: 'SMALL', text: '', AntiforgeryToken: UserInfo.AntiforgeryToken }, action) => {
    switch (action.type) {
        case 'FULL': return { size: 'FULL', text: action.text, AntiforgeryToken: UserInfo.AntiforgeryToken };
        case 'SMALL': return { size: 'SMALL', text: action.text, AntiforgeryToken: UserInfo.AntiforgeryToken };
        default: return state;
    }
};

const {createStore} = Redux;
const Store = createStore(searchStatus)

const render = () => {
    var state = Store.getState();
    ReactDOM.render(
        <Nav
            IsSignedIn={UserInfo.IsSignedIn}
            User={UserInfo.Name}
            Title={UserInfo.Title}
            SearchBarSize={state.size}
            Text={state.text}
            Store={Store}
            AntiforgeryToken={state.AntiforgeryToken}
        />, document.getElementById('nav'));
};

Store.subscribe(render);

if (UserInfo !== undefined) {
    render();
}