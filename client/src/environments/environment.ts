// TODO: make this thing come from CD/CI
const apiDomain = 'localhost:5001';

export const environment = {
    production: false,
    apiDomain: apiDomain,
    apiUrl: `https://${apiDomain}/api/v1`,
    localStorageKeys: {
        ACCESS_TOKEN_KEY: 'hiscary_access_token_key',
        REFRESH_TOKEN_KEY: 'hiscary_refresh_access_token_key',
    },
};
