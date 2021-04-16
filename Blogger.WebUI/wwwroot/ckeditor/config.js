/**
 * @license Copyright (c) 2003-2019, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see https://ckeditor.com/legal/ckeditor-oss-license
 */

CKEDITOR.editorConfig = function (config) {
    config.filebrowserImageUploadUrl = '/CkEditor/UploadImage'; //Resmin yükleneceði site adresi
    config.removePlugins = 'easyimage,cloudservices';//Easyimage, cloudervices eklentisini kapatmak için bu kod satýrýný ekleyin
    config.extraPlugins = 'codesnippet';
    config.codeSnippet_theme = 'monokai_sublime';
};
