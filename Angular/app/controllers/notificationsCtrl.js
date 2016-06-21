"use strict";

SwapNShop.controller("notificationsCtrl", [
	"$scope",
	"$http",
	"$location",
	"EquipFactory",
	"AuthFactory",

  	function ($scope, $http, $location, EquipFactory, AuthFactory) {
  		let currMusician = AuthFactory.getUser();
  		$scope.allNotifications = [];

  		$http
	        .get(`http://localhost:49881/api/Notification/${currMusician.IdMusician}`)
	        .success(notes => {
	          $scope.allNotifications = notes;
	          console.log("notifications", $scope.allNotifications)
	    	})

	    $scope.gotIt = function(id) {
	    	console.log("id", id)
	    	// Remove the notification from the page
	    	let itemToDelete = this.note;
	    	$scope.allNotifications.splice($scope.allNotifications.indexOf(itemToDelete), 1);
	    	// Delete the notification from the database
		    $http({
	            url:`http://localhost:49881/api/Notification/${id}`,
	            method: 'DELETE',
	          })
	    }

	    $scope.viewInventory = function() {
	    	$location.path("/view_inventory");
	    }
  	} 
]);