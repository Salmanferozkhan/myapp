myApp.controller('EmployeeController', ['$scope', 'EmpcrudApp', function ($scope, EmpcrudApp) {

    //EmpcrudApp.getAllEmployees().then(function (result) {
    //    $scope.Emps = result;
    //});

    $scope.getByEid = function (Eid) {
        EmpcrudApp.Details(Eid).then(function (result) {
            result.DateOfJoining = new Date(parseInt(result.DateOfJoining.substr(6)));
            $scope.Empp = result;

        });
    };
    $scope.Create = function (Empp, isValid) {
        if (isValid) {
            EmpcrudApp.Create(Empp).then(function (result) {
                if (result.Eid != null) {
                    $scope.Msg = result.EName + " Has been created successfully";
                    EmpcrudApp.getAllEmployees().then(function (result) {
                        $scope.Emps = result;
                    });
                }
                else {
                    alert(result);
                    $scope.errorMsgs = result;
                }
            });

        }
    };

    $scope.UpdateEmployee = function (Empp, isValid) {
        if (isValid) {
            EmpcrudApp.UpdateEmployee(Empp).then(function (result) {
                $scope.Msg = "Employee Name " + result + " has been updated successfully";
               
                EmpcrudApp.getAllEmployees().then(function (result) {
                    $scope.Emps = result;
                });

            });
        }
    };

    $scope.delEmpById = function (Eid) {
        EmpcrudApp.Delete(Eid).then(function (result) {
            $scope.Msg = result.Eid + " has been deleted successfully";
            EmpcrudApp.getAllEmployees().then(function (result) {
                $scope.Emps = result;
            });
        })
    }
}]);