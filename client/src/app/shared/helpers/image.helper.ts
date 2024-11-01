export const convertToBase64 = (byteArray: string, format: string = 'image/jpeg'): string => {
    return `data:${format};base64,${byteArray}`;
};
