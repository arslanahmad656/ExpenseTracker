export const formGridService = {
    getFormGridStructure: async () => {
        const response = await httpClient.get('/api/formgrid/structure');
        return response.data;
    }
}