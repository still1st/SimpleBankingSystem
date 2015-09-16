angular.module('app')
    .factory('AccountService', ['$resource', function ($resource) {
        return $resource('api/account', {}, {
            getAccounts: {
                method: 'GET', url: '/api/account/list', isArray: false
            },
            getAccountDetails: {
                method: 'GET', url: '/api/account/details', isArray: false
            },
            addAccount: {
                method: 'GET', url: '/api/account/add', isArray: false
            },
            removeAccount: {
                method: 'GET', url: '/api/account/remove?accountId=:accountId', isArray: false
            },
            getTransactions: {
                method: 'GET', url: '/api/account/transactions?accountId=:accountId', isArray: false
            },
            executeTransaction: {
                method: 'GET', url: '/api/account/transaction?accountId=:accountId&type=:type&amount=:amount', isArray: false
            }
        });
    }]);