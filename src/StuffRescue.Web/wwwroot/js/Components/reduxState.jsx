const searchStatus = (state = { size: 'SMALL', text: '' }, action) => {
    switch (action.type) {
        case 'FULL': return { size: 'FULL', text: action.text };
        case 'SMALL': return { size: 'SMALL', text: action.text };
        default: return state;
    }
};

const {createStore} = Redux;
const Store = createStore(searchStatus)

const render = () => {
    var state = Store.getState();
    ReactDOM.render(<Nav IsSignedIn={UserInfo.IsSignedIn} User={UserInfo.Name} Title={UserInfo.Title} SearchBarSize={state.size} Text={state.text} Store={Store} />, document.getElementById('nav'));
};

Store.subscribe(render);

if (UserInfo !== undefined) {
    render();
}