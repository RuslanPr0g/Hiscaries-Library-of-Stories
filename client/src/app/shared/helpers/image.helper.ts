export const convertToBase64 = (byteArray: any, format: string = 'image/jpeg'): string => {
    return `data:${format};base64,${byteArray}`;
}