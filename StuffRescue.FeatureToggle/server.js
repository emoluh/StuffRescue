'use strict';
var Connection = require('tedious').Connection;
var request = require('tedious').Request;

// Create connection to database
var config = {
    userName: '[username]', // update me
    password: '[password]', // update me
    server: '[mydbname].database.windows.net', // update me
    options: {
        database: '[mydatabase]', //update me
        encrypt: true
    }
}

const connection = new Connection(config);
connection.on('connect', function (err) {
    if (err) {
        console.log(err)
    }
    else {
        console.log('Database connneted...');
        executeStatement();
    }
});

function insertIntoDatabase() {
    console.log("Inserting a brand new feature into database...");
    request = new Request(
        "INSERT INTO dbo.[mydatabase] (Name, Value) VALUES ('DataSource', 1)",
        function (err, rowCount, rows) {
            console.log(rowCount + ' row(s) inserted');
        }
    );
    connection.execSql(request);
}

function queryDatabase() {
    console.log('Reading rows from the Table...');

    // Read all rows from table
    request = new Request(
        "SELECT ff.Name, ff.Value FROM dbo.[mydatabase] ff",
        function (err, rowCount, rows) {
            console.log(rowCount + ' row(s) returned');
        }
    );

    request.on('row', function (columns) {
        columns.forEach(function (column) {
            console.log("%s\t%s", column.metadata.colName, column.value);
        });
    });

    connection.execSql(request);
}

function executeStatement() {
    request = new Request("select 42, 'hello world'", function (err, rowCount) {
        if (err) {
            console.log(err);
        } else {
            console.log(rowCount + ' rows');
        }
    });

    request.on('row', function (columns) {
        columns.forEach(function (column) {
            console.log(column.value);
        });
    });

    connection.execSql(request);
}