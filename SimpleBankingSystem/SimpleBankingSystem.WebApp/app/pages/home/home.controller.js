angular.module('app')
    .controller('HomeCtrl', ['$scope', '$location', 'AccountService', function ($scope, $location, accountService) {

        $scope.openAccount = function (accountId) {
            $location.path('/account/' + accountId);
        };

        $scope.addAccount = function () {
            accountService.addAccount(function (result) {
                if(result.isValid)
                    $scope.accounts.push(result.data);
            });
        };

        $scope.removeAccount = function (account) {
            accountService.removeAccount({ accountId: account.accountId }, function (result) {
                if (result.isValid) {
                    var index = $scope.accounts.indexOf(account);
                    $scope.accounts.splice(index, 1);
                }
            });
        };

        $scope.loaded = false;
        accountService.getAccounts(function (result) {
            if (result.isValid)
                $scope.accounts = result.data;

            $scope.loaded = true;
        });
    }]);