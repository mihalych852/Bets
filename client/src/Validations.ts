import * as Yup from 'yup';

function configureValidation(){
    Yup.addMethod(Yup.string, 'fieldIsRequire', function(){
        return this.test('field-is-require', 
            'Поле обязательно для заполнения', function(value) {
                if(value){
                    return true;
                }
            }
        )
    })
}

export default configureValidation;