console.log("go")
SwapNShop.controller('createCtrl', [
    '$scope', 
    '$http', 
    function ($scope, $http) {
        $scope.figurine = {
            geekId: 3
        };
        $scope.createFigurine = function () {
            $http({
                url: 'http://localhost:5000/api/Inventory',
                method: 'POST',
                data: JSON.stringify($scope.figurine)
            }).success(inv => console.log('The figurine was created!'))
        }
    }
])