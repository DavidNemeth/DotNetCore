(function () {

    "user strict";

    //Getting the existing module
    angular.module("app-trips")
        .controller("tripsController", tripsController);


    function tripsController($http) {

        var vm = this;

        vm.trips = [];

        vm.newTrip = {};

        vm.errorMessage = "";
        vm.isBusy = true;


        $http.get("/api/trips")
            .then(function (response) {
                //success
                angular.copy(response.data, vm.trips);
            }, function (error) {
                //failure
                vm.errorMessage = "Failed to load data: " + error;                
            })
            .finally(function () {
                vm.isBusy = false;
            });

        vm.addTrip = function () {
            vm.trips.push({ name: vm.newTrip.name, created: new Date() });
            vm.newTrip = {};
        };

    }

})();