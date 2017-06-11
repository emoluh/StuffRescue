'use strict';
var Connection = require('tedious').Connection;
var Request = require('tedious').Request;

//Plan: Update Git Markdown
//FrontEnd: React, Bootstrap(Foundation)
//BackEnd: Express Web Server, Restful Api Framework(Loopback?)
//Test: Jasmine Framework

// Create connection to database
var config = {
    userName: '[username]', // update me
    password: '[password]', // update me
    server: '[mydbname].database.windows.net', // update me
    options: {
        database: '[mydatabase]', //update me
        tdsVersion: '7_1',
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
        queryDatabase();
    }
});

function insertIntoDatabase() {
    console.log("Inserting a brand new feature into database...");
    var request = new Request(
        "INSERT INTO dbo.[mytablename] (Name, Value) VALUES ('DataSource2', 0)",
        function (err, rowCount, rows) {
            console.log(rowCount + ' row(s) inserted');
        }
    );
    connection.execSql(request);
}

function queryDatabase() {
    console.log('Reading rows from the Table...');

    // Read all rows from table
    var request = new Request(
        "SELECT ft.Name, ft.Value FROM dbo.[mytablename] ft",
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
    var request = new Request("select 42, 'hello world'", function (err, rowCount) {
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