"use strict";

SwapNShop.controller("addEquipmentCtrl", [
  "$scope",
  "$http",
  "AuthFactory",
  'Upload',
  '$timeout',
  '$location',
  // 'ngFileUpload',

  function ($scope, $http, AuthFactory, Upload, $timeout, $location) {
  	let currMusician = AuthFactory.getUser();
    // Check to see if a user is logged in. If not, route them to log in
    if (currMusician === null){
      $location.path("/login");
    }
    $scope.equipment = {};
  	$scope.category = {};
  	// Store the uploaded images in a local array
  	let imageList = [];

  	//Get access to the current categories
  	$http
  		.get('http://localhost:49881/api/Category')
			.success(cat => {
				$scope.category = cat;
				console.log("category", $scope.category);
			});
  		

	    $scope.uploadFiles = function (files) {
        $scope.files = files;
        console.log("images to upload:", files)
        if (files && files.length) {
            Upload.upload({
                url: 'https://angular-file-upload-cors-srv.appspot.com/upload',
                data: {
                    files: files
                }
            }).then(function (response) {
                $timeout(function () {
                    $scope.result = response.data;
                });
            }, function (response) {
                if (response.status > 0) {
                    $scope.errorMsg = response.status + ': ' + response.data;
                }
            }, function (evt) {
                $scope.progress = 
                    Math.min(100, parseInt(100.0 * evt.loaded / evt.total));
            });
        }
				// Saves image as base64 in scope.image before POSTing
        Upload.base64DataUrl(files).then(
	        function (base64URLs) {
	        	for (var i = 0; i < base64URLs.length; i++) {
	        		imageList.push(base64URLs[i]);
	        	}
	          $scope.equipment.images = imageList;
	          console.log("Images successfully stored");
	        }
	      )
    };

    // function to create equipment
		$scope.createEquipment = function () {

			console.log(AuthFactory.getUser());
			// Access the current user and set it's property on the new object
			$scope.equipment.IdMusician = AuthFactory.getUser().IdMusician;
			$scope.equipment.IdCategory = $("#singleSelect").val();
			console.log("equipment",$scope.equipment)

			// post to the database
			$http({
				url:'http://localhost:49881/api/Equipment',
				method: 'POST',
				data: JSON.stringify($scope.equipment)
			})
			.success(function newEquipment (){
				console.log('201 Created', newEquipment)
				$location.path("/homepage");
			})
		};

	}
]);




    
