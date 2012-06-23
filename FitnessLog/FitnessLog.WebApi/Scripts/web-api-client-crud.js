    
var log = null;

var viewModel = {
    LogID: ko.observable(),
    Title: ko.observable(),
    Entries: ko.observableArray(),
    editEntry: function (entry) { displayEditRow(entry); },
    updateEntry: function (entry) { updateEntry(entry); },
    deleteEntry: function (entry) { deleteEntry(entry); }
};

function loadFitnessLog(id) {
    viewModel.Entries([]);

    $.ajax({
        url: '/api/fitnesslogapi/' + id,
        accepts: "application/json", 
        cache: false, 
        statusCode: { 
            200: function(data) { 
                viewModel.LogID(data.LogID);
                viewModel.Title(data.Title);
                viewModel.Entries(data.Entries);
                log = data;
            }, 
            401: function(jqXHR, textStatus, errorThrown) { 
                alert("Not Authorized to View.");
                self.location = '/'; 
            },
            404: function () {
                alert("Log not found!");
                self.location = '/'; 
            }
        }
    });
}

function displayEditRow(entry) {
    $("#EditRow" + entry.LogEntryID).show();
    $(":button[value='Edit']").hide();
    $(":button[value='Delete']").hide();
}

function updateEntry(entry) {
    $.ajax({
        url: "/api/fitnesslogapi",
        data: JSON.stringify(entry),
        type: "PUT",
        contentType: "application/json;charset=utf-8",
        statusCode: {
            200: function () {
                successUpdateCallback(entry);
            },
            404: function () {
                alert("Entry not found!");
            }
        }
    });
}

function deleteEntry(entry) {
    $.ajax({
        url: "/api/fitnesslogapi",
        data: JSON.stringify(entry),
        type: "DELETE",
        contentType: "application/json;charset=utf-8",
        statusCode: {
            200: function () {
                successDeleteCallback();
            },
            404: function () {
                alert("Entry not found!");
            }
        }
    });
}

function addEntry() {
    var entry = {
        ExerciseName: $('#NewExerciseName').val(),
        DateAndTime: $('#NewDateTime').val(),
        Lbs: $('#NewLbs').val(),
        Reps: $('#NewReps').val(),
        Log: log
    };

    $.ajax({
        url: "/api/fitnesslogapi",
        data: JSON.stringify(entry),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        statusCode: {
            200: function () {
                successInsertCallback();
            },
            400: function (jqxhr) { 
                var validationResult = $.parseJSON(jqxhr.responseText);       
                alert(validationResult.ExerciseName);
            } 
        }
    });
}

function successUpdateCallback(entry) {
    $("#EditRow" + entry.LogEntryID).hide();
    $(":button[value='Edit']").show();
    $(":button[value='Delete']").show();

    loadFitnessLog(request.id);
}

function successDeleteCallback(entry) {
        
    loadFitnessLog(request.id);
}

function successInsertCallback() {
    //viewModel.push(entry);  

    $('#NewExerciseName').val('');
    $('#NewDateTime').val('');
    $('#NewLbs').val('');
    $('#NewReps').val('');

    loadFitnessLog(request.id);
}

$(document).ready(function () {

    // activate knockout
    ko.applyBindings(viewModel);

    // load data
    loadFitnessLog(request.id);
});