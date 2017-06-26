const searchStatus = (state = 'SMALL', action) => {
    switch (action.type) {
        case 'FULL': return 'FULL';
        case 'SMALL': return 'SMALL';
        default: return state;
    }
};

const {createStore} = Redux;
const Store = createStore(searchStatus)

const render = () => {
    ReactDOM.render(<Nav IsSignedIn={UserInfo.IsSignedIn} User={UserInfo.Name} Title={UserInfo.Title} SearchBarSize={Store.getState()} Store={Store} />, document.getElementById('nav'));
};

Store.subscribe(render);

if (UserInfo !== undefined) {
    render();
}