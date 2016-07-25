var myApp = angular.module('myApp', ['timepickerPop', 'ngAnimate', 'ui.bootstrap', 'ngRoute']);

myApp.config(["$routeProvider",function ($routeProvider) {
    $routeProvider
    .when("/Employees", {
        templateUrl: "templates/Employees/Employee.cshtml",
        controller: "EmployeeController"
    })
    .when("/Home", {
        templateUrl: "templates/Home/Home.cshtml"
    })

.when("/EmployeeProgress", {
    templateUrl: "templates/EmployeeStats/EmployeeStats.cshtml"
})
    .otherwise({
        templateUrl: "templates/Home/Home.cshtml"

    });

}]);
myApp.factory('crudApp',["$http", function ($http) {

    crudApp = {};

    crudApp.getAllEmployees = function () {
        var emp;

        emp = $http({ method: 'Get', url: '/Home/GetEmployees' })
        .then(function (response) {
            //alert('Working');
            return response.data;
        });

        return emp;
    };

    crudApp.delEmpbyID = function (Aid) {
        var Man;

        Man = $http({ method: 'POST', url: '/Home/DeleteEmployee', params: { Aid: Aid } }).
        then(function (response) {
            return response.data;
        });

        return Man;
    };
    crudApp.getEmployeesToday = function () {
        var emp;

        emp = $http({ method: 'Get', url: '/Home/GetEmpToday' })
        .then(function (response) {
            return response.data;
        });
        return emp;
    };
    crudApp.EmployeeIN = function (Emp, e) {
        var emp;

        emp = $http({ method: 'POST', url: '/Home/EmployeeIN', params: { IN: Emp, e: e } }).
        then(function (response) {
            return response.IN;
        });

        return emp;
    };
    crudApp.getByAid = function (Aid) {
        var Emp;

        Emp = $http({ method: 'Get', url: '/Home/Details/', params: { Aid: Aid } }).
        then(function (response) {
            return response.data;
        });
        return Emp;
    }
    crudApp.UpdateAttandence = function (emp) {
        var Emp;

        Emp = $http({ method: 'POST', url: '/Home/Edit/', data: emp }).
        then(function (response) {
            return response.data;
        });
        //selectors
        //JQuery => $("#abc").val(); 
        //Javascript => document.getElementById('abc').value;
        return Emp;
    };
    crudApp.EmployeeOUT = function (outtime, Eid) {
        var emp;

        emp = $http({ method: 'POST', url: '/Home/EmployeeOUT', params: { OUT: outtime, Eid: Eid } }).
        then(function (response) {
            alert(response.Eid);
            return response.IN;
        });

        return emp;
    };
    return crudApp;
}]);
myApp.factory('EmpcrudApp',["$http", function ($http) {

    EmpcrudApp = {};

    EmpcrudApp.getAllEmployees = function () {
        var emp;

        emp = $http({ method: 'Get', url: '/Employees/GetAllEmployees' })
        .then(function (response) {
            return response.data;
        });

        return emp;
    };
    EmpcrudApp.Details = function (Eid) {
        var Man;

        Man = $http({ method: 'POST', url: '/Employees/Details', params: { Eid: Eid } }).
        then(function (response) {
            return response.data;
        });

        return Man;
    };
    EmpcrudApp.Create = function (Emp) {
        var emp;

        emp = $http({ method: 'POST', url: '/Employees/Create', data: Emp }).
        then(function (response) {
            return response.data
        });

        return emp;
    };
    EmpcrudApp.UpdateEmployee = function (emp) {
        var Emp;

        Emp = $http({ method: 'POST', url: '/Employees/Edit/', data: emp }).
        then(function (response) {
            return response.data;
        });

        return Emp;
    };
    EmpcrudApp.Delete = function (Eid) {
        var Man;

        Man = $http({ method: 'POST', url: '/Employees/Delete', params: { Eid: Eid } }).
        then(function (response) {
            return response;
        });

        return Man;
    };
    EmpcrudApp.getEmployeesStats = function (Emp) {
        var emp;

        emp = $http({ method: 'Get', url: '/ShowAttandence/GetEmpByMonth', params: { IN: Emp } })
        .then(function (response) {
            return response.data;
        });

        return emp;
    };
    EmpcrudApp.getEmployeesbyYear = function (Emp) {
        var emp;

        emp = $http({ method: 'Get', url: '/ShowAttandence/Index', params: { IN: Emp } })
        .then(function (response) {
            return response.data;
        });

        return emp;
    };


    return EmpcrudApp;
}]);

myApp.filter('jsonDate', ['$filter', function ($filter) {
    return function (input, format) {
        return (input) ? $filter('date')(parseInt(input.substr(6)), format) : '';
    };
}]);

myApp.controller('ModalController2',['$scope','$uibModalInstance','time', function ($scope, $uibModalInstance, time) {
    $scope.mytime2 = new Date();

    $scope.hstep = 1;
    $scope.mstep = 15;
    $scope.mytime2 = '';

    $scope.options = {
        hstep: [1, 2, 3],
        mstep: [1, 5, 10, 15, 25, 30]
    };
    $scope.ismeridian = false;
    $scope.mytime2 = time;
    $scope.selected = {
        mytime2: $scope.mytime2
    };
    $scope.toggleMode = function () {
        $scope.ismeridian = !$scope.ismeridian;
    };

    $scope.update = function () {
        var d = new Date();
        d.setHours(18);
        d.setMinutes(0);
        $scope.mytime2 = d;
    };

    $scope.changed2 = function () {
        console.log('Time changed to: ' + $scope.mytime2);
    };

    $scope.clear = function () {
        $scope.mytime2 = null;
    };
    $scope.ok2 = function () {
        $uibModalInstance.close($scope.mytime2);
    };

    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };

}]);
//myApp.controller('ModalInstanceCtrl', function ($scope, $uibModalInstance, time) {
//    $scope.mytime = new Date();
myApp.directive('formatDate', function () {

    return {
        require: 'ngModel',

        link: function (scope, element, attr, ngModelController) {
            ngModelController.$formatters.unshift(function (valueFromModel) {

                if (angular.isUndefined(valueFromModel)) {
                    return valueFromModel;
                }

                var date = new Date(parseInt(valueFromModel.substr(6)));
                console.log(valueFromModel);
                return date.toLocaleDateString();
            });
        }
    };
});