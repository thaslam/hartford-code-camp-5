var viewModel = {
    Logs: ko.observableArray()
};

function LoadLogsFromServer() {
    viewModel.Logs([]);

    //'/api/fitnesslogapi?key=dotnet', 

    $.get(
                '/api/fitnesslogapi',
                function (data) {
                    viewModel.Logs(data);
                },
                'json'
            );
}

$(document).ready(function () {

    // activate knockout
    ko.applyBindings(viewModel);

    LoadLogsFromServer();
});     