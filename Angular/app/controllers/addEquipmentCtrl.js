"use strict";

SwapNShop.controller("addEquipmentCtrl", [
  "$scope",
  "$http",
  "AuthFactory",
  // 'Upload',
  // '$timeout',
  // 'ngFileUpload',

  function ($scope, $http, AuthFactory) {
  	$scope.equipment = {};
  	$scope.category = {};

  	//Get access to the current categories
  	$http
  		.get('http://localhost:49881/api/Category')
			.success(cat => {
				$scope.category = cat;
				console.log("category", $scope.category);
			});
  	

  	// function to create equipment
		$scope.createEquipment = function () {

			console.log(AuthFactory.getUser());
			// Access the current user and set it's property on the new object
			$scope.equipment.IdMusician = AuthFactory.getUser().IdMusician;
			$scope.equipment.IdCategory = $("#singleSelect").val();
			console.log("val", $("#singleSelect").val())

			// post to the database
			$http({
				url:'http://localhost:49881/api/Equipment',
				method: 'POST',
				data: JSON.stringify($scope.equipment)
			})
			.success(newEquipment => console.log('201 Created', newEquipment))
		
		};

		// $scope.uploadFiles = function (files) {
  //       $scope.files = files;
  //       if (files && files.length) {
  //           Upload.upload({
  //               url: 'https://angular-file-upload-cors-srv.appspot.com/upload',
  //               data: {
  //                   files: files
  //               }
  //           }).then(function (response) {
  //               $timeout(function () {
  //                   $scope.result = response.data;
  //               });
  //           }, function (response) {
  //               if (response.status > 0) {
  //                   $scope.errorMsg = response.status + ': ' + response.data;
  //               }
  //           }, function (evt) {
  //               $scope.progress = 
  //                   Math.min(100, parseInt(100.0 * evt.loaded / evt.total));
  //           });
  //       }
  //   };

	}
]);




    
