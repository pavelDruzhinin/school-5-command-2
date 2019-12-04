<template>
    <div>
        <div class="error" v-if="!v.form.Password.required">Введите поле</div>
        <div class="error" v-if="!v.form.Password.minLength">Минимальный размер пароля составляет 
            {{v.form.Password.$params.minLength.min}} символа
        </div>
    </div>
</template>

<script>
export default {
    props:{
        v:{
            type:Object,
            required:true
        }
    }
}
</script>