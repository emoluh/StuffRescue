class SearchField extends React.Component {
    render() {
        return (
            <div className="search">
                <span className="material-icons">search</span>
                <input
                    type="text"
                    placeholder="What are you looking to rescue?"
                    name={this.props.name}
                    autoComplete="off"
                    onChange={this.props.onChange} />
            </div>
        );
    }
}

class SearchFullScreen extends React.Component {
    constructor(props) {
        super(props);
    }

    componentDidMount() {
        this._input.value = this.props.Text;
    }

    render() {
        return (
            <div className="full-screen-search">
                <button
                    className="close-button"
                    onClick={this.props.onClose}>X</button>
                <span
                    style={{ fontSize: "2.5em", color: "#F79636", fontWeight: "1.5em" }}
                    className="material-icons">search</span>
                <input
                    ref={f => this._input = f}
                    type="text"
                    name="searchlarge"
                    onChange={this.props.onChange} />
            </div>
        );
    }
}

class SearchFullScreenForm extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            showLargeSearch: {
                display: 'block'
            },
            Store: props.Store
        };

        this._handleChange = this._handleChange.bind(this);
        this._handleClose = this._handleClose.bind(this);
    }

    componentDidMount() {
        this._form.searchlarge.focus();
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
        this.setState({ showLargeSearch: { display: 'none' } });
        Store.dispatch({ type: 'SMALL', text: this._form.searchlarge.value });
    }

    render() {
        return (
            <div>
                <form ref={f => this._form = f}>
                    <div style={this.state.showLargeSearch}>
                        <SearchFullScreen
                            onChange={this._handleChange}
                            onClose={this._handleClose}
                            Text={this.props.Text}
                        />
                    </div>
                </form>
            </div>
        );

    }
}

class Search extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            Store: props.Store
        };

        this._handleChange = this._handleChange.bind(this);
    }

    componentDidMount() {
        this._form.search.value = this.props.Text;
    }
    _handleChange(e) {
        e.preventDefault();
        Store.dispatch({ type: 'FULL', text: e.target.value });
    }

    render() {
        return (
            <div>
                <form ref={f => this._form = f}>
                    <SearchField
                        name="search"
                        onChange={this._handleChange}
                        Text={this.props.Text}
                    />
                </form>
            </div>
        );
    }
}

export default { SearchFullScreenForm, Search };
