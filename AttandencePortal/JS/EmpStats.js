myApp.controller('EmpStatsController', ['$scope', 'EmpcrudApp', function ($scope, EmpcrudApp) {

    $scope.getEmployeesbyMonth = function (Emp) {
        EmpcrudApp.getEmployeesStats(Emp).then(function (result) {
            $scope.EmpStats = result;
        })
    };

    $scope.getEmployeesbyYear = function (Emp) {
        EmpcrudApp.getEmployeesbyYear(Emp).then(function (result) {
            $scope.EmpStats = result;
        })
    };

}]);