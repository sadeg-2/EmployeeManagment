window.downloadFileFromStream = async (filename, streamReference) => {
    const arrayBuffer = await streamReference.arrayBuffer();
    const blob = new Blob([arrayBuffer]);
    const url = URL.createObjectURL(blob);
    const anchorElem = document.createElement("a");
    anchorElem.href = url;
    anchorElem.download = filename ?? "download.xlsx";
    anchorElem.click();
    anchorElem.remove();
    URL.revokeObjectURL(url);
};
