import { ChangeEventHandler, ComponentType } from 'react';
import { IconProps } from '../icons/IconProps';
import styles from './AuthInput.module.css';

type AuthInputProps = {
  type: string;
  placeholder: string;
  value: string;
  changeHandler?: ChangeEventHandler<HTMLInputElement>;
  Icon: ComponentType<IconProps>;
  readOnly?: boolean;
};

export function AuthInput({
  type,
  placeholder,
  value,
  Icon,
  changeHandler,
  readOnly,
}: AuthInputProps) {
  return (
    <div className={styles.formGroup}>
      <div className={styles.iconContainer}>
        <Icon className={styles.icon} />
      </div>
      <input
        type={type}
        placeholder={placeholder}
        value={value}
        onChange={changeHandler}
        readOnly={readOnly}
      />
    </div>
  );
}
